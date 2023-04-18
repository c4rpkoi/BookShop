
function listSearchExamplesScript() {

    var value = $("#SearchFieldId").val();

    $.ajax({
        type: 'GET',
        url: '/Books/AjaxSearchList',
        data: { searchString: value }
    })
        .done(function (result) {
            $("#SuggestOutput").html(result);
            $("#BookSummaryId").remove();
        })

        .fail(function (xhr, status, error) {
            $("#SuggestOutput").text("No matches where found.");
        });
}
