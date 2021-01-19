$(document).ready(function () {
    let fStarsVote = new fiveStarsVote();
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
            document.getElementById("LastCooked").setAttribute("value", result)
        },
        error: function (result) {
            //TODO 
            var error = result;
        }
    });
}
