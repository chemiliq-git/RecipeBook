$(document).ready(function () {
    let searchViewSideBar = new sideBar();
    searchViewSideBar.init("Ingredient");
    searchViewSideBar.addEventListener('complete', (e: CustomEvent) => {

        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let data = e.detail.formData as FormData;

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
                var error = result;
            }
        });
    })
});