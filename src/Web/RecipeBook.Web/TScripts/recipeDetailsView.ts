$(document).ready(function () {
    let fTasteStarsVote = new FiveStarsVote("TasteRateStars");
    let fEasyStarsVote = new FiveStarsVote("EasyRateStars");
});

function OnCookedTodayClick(Id: string) {
    let token = $("#keyForm input[name=__RequestVerificationToken]").val();
    let data = new FormData();
    data.append("id", Id);

    $.ajax({
        url: "/recipe/UpdateLastCookedDate",
        processData: false,
        contentType: false,
        data: data,
        type: "POST",
        headers: { 'X-CSRF-TOKEN': token.toString() },
        success: function (result) {   
            if (result.result != '') {
                document.getElementById("LastCooked").setAttribute("value", result.result)
            }
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
