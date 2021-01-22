$(document).ready(function () {
    let searchViewSideBar = new sideBar(onSideBarChange);
    searchViewSideBar.startListenOnSideBarChange();
    let fStarsVote = new fiveStarsVote();
    fStarsVote.startListenToVote();

    function onSideBarChange(data: any) {

        let token = $("#keyForm input[name=__RequestVerificationToken]").val();

        $.ajax({
            url: "/Home/SideBarSearch",
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


