$(document).ready(function () {
    let searchViewSideBar = new sideBar(onSideBarChange, "Recipe");
    searchViewSideBar.startListenOnSideBarChange();
    let fTasteStarsVote = new fiveStarsVote("TasteRateStars");
    fTasteStarsVote.startListenToVote();
    let fEasyStarsVote = new fiveStarsVote("EasyRateStars");
    fEasyStarsVote.startListenToVote();

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
                let fTasteStarsVote = new fiveStarsVote("TasteRateStars");
                fTasteStarsVote.startListenToVote();
                let fEasyStarsVote = new fiveStarsVote("EasyRateStars");
                fEasyStarsVote.startListenToVote();

            },
            error: function (result) {
                //TODO 
                var error = result;
            }
        });
    }
});


