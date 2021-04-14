$(document).ready(function () {
    const INGREDIENTS_SIDEBAR_SEARCH_URL = "/Ingredients/SideBarSearch";
    let searchViewSideBar = new SideBar();
    searchViewSideBar.init(AutoCompleteSearchModeEnum.Ingredient.toString());
    searchViewSideBar.addEventListener(Const.SIDE_BAR_COMPLETE_EVENT_NAME, (e) => {
        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let data = e.detail.formData;
        $.ajax({
            url: INGREDIENTS_SIDEBAR_SEARCH_URL,
            data: data,
            processData: false,
            contentType: false,
            type: Const.POST,
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: (result) => {
                $('#' + Const.PARTIAL_VIEW_ID).html(result);
            },
            error: function (result) {
                //TODO 
                let error = result;
            }
        });
    });
});
//# sourceMappingURL=ingredientIndexView.js.map