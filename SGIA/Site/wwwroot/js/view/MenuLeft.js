$(function () {
    $.ajax({
        method: 'GET', 
        url: '/Home/MenuLeft',
        dataType: 'html',
        success: function (content) {
            $("#MenuLeft").html(content);
        }
    });
})