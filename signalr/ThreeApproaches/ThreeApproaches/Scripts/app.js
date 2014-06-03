$(document).ready(function () {
	//
    if (location.search == "?user") {
        var userName = prompt("User name?")
        if (userName) {
            $.cookie("username", userName)
        } else {
            $.removeCookie("username")
        }
        var url = document.location.href.substr(0, document.location.href.indexOf("?"))
        document.location = url
    }
    $('#help-button').on('click', function () {
        $('#helpDeskModal').modal('show')
    })
    $('#help-desk-submit').on('click', function () {
        $.cookie('username', $('#help-name').val())
        $.connection.messageHub.server.help({
            Message: $('#help-message').val(),
            Requester: $('#help-name').val(),
            CurrentPage: document.location.href
        })
        $('#helpDeskModal').modal('hide')
    })
    hubSubscriptions.on('helpRequested', function (helpRequest) {
        var initialMessage = $("<p><strong>" + helpRequest.Requester + "</strong>"
            + " needs help with <code>" + helpRequest.CurrentPage + "</code>:"
            + " <blockquote>" + helpRequest.Message + "</blockquote>");
        $('#help-chat').append(initialMessage);
        $('#helpDeskChat').modal('show')
    })
})