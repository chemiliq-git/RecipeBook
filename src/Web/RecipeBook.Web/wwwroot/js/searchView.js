$(document).ready(function () {
    var searchViewSideBar = new sideBar(onSideBarChange);
    searchViewSideBar.startListenOnSideBarChange();
    initStars();
});
function initStars() {
    var allElements = $('[id^="stars"]').toArray();
    allElements.forEach(function (Element) {
        var starsElementData = $(Element).data('value');
        var dataArray = starsElementData.split(',');
        var tasteRate = dataArray[1];
        var stars = $(Element).children('li.star');
        for (var i = 0; i < stars.length; i++) {
            if (i < parseInt(tasteRate)) {
                $(stars[i]).addClass('selected');
            }
            else {
                $(stars[i]).removeClass('selected');
            }
        }
    });
}
function onSideBarChange(data) {
    var token = $("#keyForm input[name=__RequestVerificationToken]").val();
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
/* 1. Visualizing things on Hover - See next part for action on click */
function onStartMouseOver(currentElement) {
    var onStar = parseInt($(currentElement).data('value'), 10); // The star currently mouse on
    // Now highlight all the stars that's not after the current hovered star
    $(currentElement).parent().children('li.star').each(function (index) {
        if (index < onStar) {
            $(this).addClass('hover');
        }
        else {
            $(this).removeClass('hover');
        }
    });
}
;
function onStartMouseOut(currentElement) {
    $(currentElement).parent().children('li.star').each(function (e) {
        $(this).removeClass('hover');
    });
}
;
/* 2. Action to perform on click */
function onStarClick(currentElement) {
    var onStar = parseInt($(currentElement).data('value'), 10); // The star currently selected
    //POST
    var parentElementData = $(currentElement).parent().data('value');
    var array = parentElementData.split(',');
    var recipeId = array[0];
    var token = $("#keyForm input[name=__RequestVerificationToken]").val();
    var data = new FormData();
    data.append("RecipeId", recipeId);
    data.append("Type", "Taste");
    data.append("Value", onStar.toString());
    $.ajax({
        url: "/api/vote",
        data: data,
        processData: false,
        contentType: false,
        type: "POST",
        headers: { 'X-CSRF-TOKEN': token.toString() },
        success: function (result) {
            var stars = $(currentElement).parent().children('li.star');
            $('#taste_rate').html(result);
            for (var i = 0; i < stars.length; i++) {
                if (i < parseInt(result)) {
                    $(stars[i]).addClass('selected');
                }
                else {
                    $(stars[i]).removeClass('selected');
                    $(stars[i]).removeClass('hover');
                }
            }
        },
        error: function (result) {
            //TODO 
            var error = result;
        }
    });
}
;
//# sourceMappingURL=searchView.js.map