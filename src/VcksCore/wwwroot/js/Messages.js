$(document).ready(function () {
    
    twemoji.size = '16x16';

    chat = $.connection.messagesHub;

    chat.client.refreshDialog = function (messages) {
        var messagesWindow = $('#MessagesWindow').empty();
        var content = $('<div></div>');

        for (i = 0; i < messages.length; ++i) {
            var m1 = $('<div></div>');
            var incoming = CurrentUserId != messages[i].FromId;
            if (!incoming)
                m1.addClass('OutgoingMessage'); else m1.addClass('IncomingMessage');
            var m2 = $('<div></div>').addClass('Message').html(messages[i].Body);
            if (incoming) {
                m2.addClass('read')
            }
            else {
                if (messages[i].Read)
                    m2.addClass('read'); else m2.addClass('unread');
            }
            m1.append(m2);
            content.append(m1);
        }

        messagesWindow.html(twemoji.parse(content.html()));

        messagesWindow.scrollTop(messagesWindow[0].scrollHeight);

        chat.server.markAsRead(messages[messages.length - 1].Id);
    };

    chat.client.refreshDialogs = function (dp) {
        var dialogsPanel = $('#DialogsPanel').empty();
        var content = $('<div></div>');

        for (i = 0; i < dp.Dialogs.length; ++i) {

            var d1 = $('<div></div>').addClass('MessagesDialogPanel').attr('onclick', 'GetDialog(' + dp.Dialogs[i].UserId + ')').attr('id', 'd' + dp.Dialogs[i].UserId);
            var d2 = $('<div></div>').addClass('row nopadding');
            var d31 = $('<div></div>').addClass('col-xs-12 col-sm-12 col-md-3 col-lg-3 nopadding');
            var d31i = $('<img>').addClass('img-responsive MessagesDialogPanelImage').attr('style', 'width:auto;height:auto').attr('src', dp.Dialogs[i].Avatar100);

            var d32 = $('<div></div>').addClass('hidden-xs hidden-sm col-md-9 col-lg-9 nopadding');
            var d32d0 = $('<div></div>').addClass('MessagesDialogPanelLayout');

            var d32d0d1 = $('<div></div>').addClass('MessagesDialogPanelName');
            var d32d0d1a = $('<a></a>').addClass('DefaultLink').attr('href', '/Profile/' + dp.Dialogs[i].UserId).html(dp.Dialogs[i].FullName);
            var d32d0d1as = $('<span></span>').addClass('CountOfNewMessagesInDialog').attr('style', 'font-weight:bold;color:white').html(' +' + dp.Dialogs[i].UnreadMessagesCount);
            var d32d0d2 = $('<div></div>').addClass('MessagesDialogPanelBody').html(dp.Dialogs[i].TrimmedBody);

            d31.append(d31i);
            d2.append(d31);
            if (dp.Dialogs[i].UnreadMessagesCount > 0 && dp.Dialogs[i].UserId != SelectedDialog) d32d0d1a.append(d32d0d1as);
            d32d0d1.append(d32d0d1a);
            d32d0.append(d32d0d1);
            d32d0.append(d32d0d2);
            d32.append(d32d0);
            d2.append(d32);
            d1.append(d2);
            content.append(d1);
        }

        dialogsPanel.html(twemoji.parse(content.html()));
            
        AddEventListener();
    };

    $.connection.hub.start().done(function () {
        chat.server.connect();
        AddEventListener();
        $('#MessagesWindow').empty();
        if (SelectedDialog != 0)
            chat.server.getDialog(SelectedDialog);
    });
});

function AddEventListener() {
    $('.MessagesDialogPanel').click(function () {
        $('.MessagesDialogPanel .MessagesDialogPanelImage').removeClass('myShadow');
        $(this).find('.MessagesDialogPanelImage').addClass('myShadow');
        $('.MessagesDialogPanel .MessagesDialogPanelLayout').removeClass('myShadow');
        $(this).find('.MessagesDialogPanelLayout').addClass('myShadow');
    });

    $('.MessagesDialogPanelName a').click(function (event) {
        event.stopPropagation();
    });

    HighlightDialog();
}

function HighlightDialog() {
    if (SelectedDialog != 0) {
        $('#d' + SelectedDialog).find('.MessagesDialogPanelImage').addClass('myShadow');
        $('#d' + SelectedDialog).find('.MessagesDialogPanelLayout').addClass('myShadow');
    }
}

function GetDialog(id) {
    if (SelectedDialog != id) {
        SelectedDialog = id;
        chat.server.getDialog(id);
        $('#d' + id + ' .CountOfNewMessagesInDialog').hide();
    }
}

function SendMessage() {
    var text = $('#MessagesTextField');
    chat.server.sendMessage(SelectedDialog, text.val());
    text.val('');
}