$(document).ready(function () {
    let fTasteStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_TASTE_RATE_ELEMENT_ID);
    let fEasyStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_EASY_RATE_ELEMENT_ID);
});
function OnCookedTodayClick(Id) {
    const LAST_COOKED_ELEMENT_ID = 'LastCooked';
    let token = $("#keyForm input[name=__RequestVerificationToken]").val();
    let data = new FormData();
    data.append("id", Id);
    $.ajax({
        url: Const.RECIPE_SIDEBAR_UPADTE_LAST_COOKED_URL,
        processData: false,
        contentType: false,
        data: data,
        type: Const.POST,
        headers: { 'X-CSRF-TOKEN': token.toString() },
        success: function (result) {
            if (result.result != '') {
                document.getElementById(LAST_COOKED_ELEMENT_ID).setAttribute(Const.HTML_ATTRIBUTE_VALUE_KEY, result.result);
            }
        },
        error: function (error) {
            if (error.status == Const.NO_AUTHENTICATION_ERROR_NUMBER) {
                window.location.href = Const.IDENTITY_LOGIN_URL;
            }
            else {
                //TODO show custom error msg
            }
        }
    });
}
//# sourceMappingURL=recipeDetailsView.js.map