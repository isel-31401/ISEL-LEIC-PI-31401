$(function () {
    $("#tags").keypress(
            function () {
                var url = "/CUF/Official/SearchTags?SearchValue=" + $("#tags").val();
                $.get(url, function (tagsList) {
                    $("#tags").autocomplete({ source: tagsList });
                });
            }
        );
});