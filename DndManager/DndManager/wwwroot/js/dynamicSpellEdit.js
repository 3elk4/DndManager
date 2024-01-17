$("#addRow").click(function () {
    $.ajax({
        url: this.href,
        cache: false,
        success: function (html) {
            $("#editorRows").append(html);
        }
    });
    return false;
});

$("a.deleteRow").on("click", function () {
    var spelllvlid = $("input[name='SpellLvlInfoId']").val();
    var url = $(this).data("value");

    $(this).parents("div.editorItem:first").remove();

    $.ajax({
        url: url,
        method: 'post',
        cache: false,
        data: JSON.stringify({ "spelllvlid": spelllvlid }),
        dataType: 'JSON',
        processData: false,
        headers:
        {
            "RequestVerificationToken": $("input[name='__RequestVerificationToken']").val()
        },
        contentType: 'application/json',
        success: function (response) {
            window.location.href = response.redirectToUrl + '?errorCode=' + response.error;
        }
    });
    return false;
});

function saveData(url, jsonObject) {
    $.ajax({
        method: 'post',
        url: url,
        data: JSON.stringify(jsonObject),
        dataType: 'JSON',
        processData: false,
        headers:
        {
            "RequestVerificationToken": $("input[name='__RequestVerificationToken']").val()
        },
        contentType: 'application/json',
        success: function (response) {
            window.location.href = response.redirectToUrl + '?errorCode=' + response.error;
        }
    });
};