$(document).ready(function () {
    let searchViewSideBar = new sideBar(onSideBarChange, "Ingredient");
    searchViewSideBar.startListenOnSideBarChange();

    function onSideBarChange(data: any, context) {

        let token = $("#keyForm input[name=__RequestVerificationToken]").val();

        $.ajax({
            url: "/Ingredients/SideBarSearch",
            data: data,
            processData: false,
            contentType: false,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: function (result) {
                $('#partialView').html(result);
            },
            error: function (result) {
                //TODO 
                var error = result;
            }
        });
    }

});