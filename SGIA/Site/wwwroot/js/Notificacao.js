$(function () {
    //$.ajax({
    //    method: 'GET',
    //    url: '/Login/Notifications',
    //    success: function (retorno) {
    //        notificacao(retorno);
    //    }
    //});

    $.ajax({
        method: 'GET',
        url: '/Admin/Home/Notifications',
        success: function (retorno) {
            notificacao(retorno);
        }
    });
});

function notificacao(retorno) {
    if (typeof window.stackModalLeft === 'undefined') {
        window.stackModalLeft = {
            'dir1': 'down',
            'dir2': 'right',
            'firstpos1': 25,
            'firstpos2': 25,
            'push': 'top',
            'modal': true,
            'overlayClose': true
        };
    }

    if (typeof window.stackBarTop === 'undefined') {
        window.stackBarTop = {
            'dir1': 'down',
            'firstpos1': 0,
            'spacing1': 0,
            'push': 'top'
        };
    }

    var opts = {
        title: '',
        text: "",
        addClass: 'stack-bar-top',
        cornerClass: 'ui-pnotify-sharp',
        shadow: false,
        width: '100%',
        stack: window.stackBarTop
    };

    for (var i = 0; i < retorno.list.length; i++) {
        switch (retorno.list[i].type) {
            case 'Error':
                opts.title = retorno.list[i].message;
                opts.text = '';
                opts.type = 'error';
                break;
            case 'Info':
                opts.title = retorno.list[i].message;
                opts.text = '';
                opts.type = 'info';
                break;
            case 'Success':
                opts.title = retorno.list[i].message;
                opts.text = '';
                opts.type = 'success';
                break;
        }
    }

    if (retorno.list.length > 0) {
        PNotify.alert(opts);
    }
}