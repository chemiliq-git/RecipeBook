$(document).ready(function () {
    let searchViewSideBar = new sideBar();
    searchViewSideBar.init(AutoCompleteSearchModeEnum.Ingredient.toString());
    searchViewSideBar.addEventListener('complete', (e) => {
        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let data = e.detail.formData;
        $.ajax({
            url: "/Ingredients/SideBarSearch",
            data: data,
            processData: false,
            contentType: false,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: (result) => {
                $('#partialView').html(result);
            },
            error: function (result) {
                //TODO 
                let error = result;
            }
        });
    });
});
//# sourceMappingURL=ingredientIndexView.js.map