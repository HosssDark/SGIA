$(function () {
    $('.Excluir').on('click', function () {
        var data_id = $(this).data('id');
        var data_url = $(this).data('url');

        Excluir(data_url, data_id);
    });

    RenderAction();
});

function Excluir(Url, Id) {

    var token = $("input[type = hidden][name = __RequestVerificationToken]").val();

    var data = {
        __RequestVerificationToken: token,
        Id: Id
    }

    swal({
        title: "Deseja Excluir o Registro?",
        text: "",
        type: "info",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sim",
        cancelButtonText: "Não",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                method: 'POST',
                url: Url,
                data: data,
                success: function (data) {
                    if (data.Result = "OK") {
                        showNotification("alert-success", null, "bottom", "center", animateEnter, animateExit);
                    } else {
                        showNotification("alert-danger", null, "bottom", "center", animateEnter, animateExit);
                    }
                }
            });
        } else {
            swal("Cancelado", "", "error");
        }
    });
}

function RenderAction() {

   var element = document.getElementsByClassName('RenderAction');

    for (var i = 0; i < element.length; i++) {

        var Url = element[i].dataset.url;
        var Class = "." + element[i].className;

        $.ajax({
            method: 'GET',
            url: Url,
            dataType: 'html',
            success: function (content) {
                $(Class).html(content);
            }
        });
    }
}