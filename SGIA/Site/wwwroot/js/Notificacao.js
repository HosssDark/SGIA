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
    var opts = {
        title: '',
        text: "",
        stack: window.stackModalLeft
    };

    for (var i = 0; i < retorno.list.length; i++) {

        for (var j = 0; j < retorno.list[i].message.length; j++) {
            switch (retorno.list[i].type) {
                case 'Error':
                    opts.title = retorno.list[i].message[j];
                    opts.text = '';
                    opts.type = 'error';
                    break;
                case 'Info':
                    opts.title = retorno.list[i].message[j];
                    opts.text = '';
                    opts.type = 'info';
                    break;
                case 'Success':
                    opts.title = retorno.list[i].message[j];
                    opts.text = '';
                    opts.type = 'success';
                    break;
            }
        }
    }

    if (retorno.list.length > 0) {
        PNotify.alert(opts);
    }
}