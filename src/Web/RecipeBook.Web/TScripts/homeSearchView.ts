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

function OnMenuClick(id: string) {
    let isAdded = $(document.getElementById('MenuButton_' + id)).data('value')
    let token = $("#keyForm input[name=__RequestVerificationToken]").val();
    let data = new FormData();
    data.append("id", id);

    if (isAdded) {
        $.ajax({
            url: "/home/AddRecipeToMenu",
            processData: false,
            contentType: false,
            data: data,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: function (result) {
                let btnElement: HTMLElement = document.getElementById('MenuButton_' + result.id)
                btnElement.setAttribute("class", "btn btn-sm btn-danger rounded-pill")
                $(btnElement).data('value', false)
                document.getElementById('MenuButton_' + result.id).children[0].setAttribute("class", "fas fa-minus-circle")
                                
            },
            error: function (error) {
                if (error.status == 401) {
                    window.location.href = '/Identity/Account/Login';
                }
                else {
                    //TODO show custom error msg
                }
            }
        });
    }
	else {
        $.ajax({
            url: "/home/RemoveRecipeFromMenu",
            processData: false,
            contentType: false,
            data: data,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: function (result) {
                let btnElement: HTMLElement = document.getElementById('MenuButton_' + result.id)
                btnElement.setAttribute("class", "btn btn-sm btn-success rounded-pill")
                $(btnElement).data('value', true)
                document.getElementById('MenuButton_' + result.id).children[0].setAttribute("class", "fas fa-plus-circle")
            },
            error: function (error) {
                if (error.status == 401) {
                    window.location.href = '/Identity/Account/Login';
                }
                else {
                    //TODO show custom error msg
                }
            }
        });
	}
}


