$(document).ready(function () {
    var searchViewSideBar = new sideBar(onSideBarChange);
    searchViewSideBar.startListenOnSideBarChange();
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
    $('#stars li').on('mouseover', function () {
        var onStar = parseInt($(this).data('value'), 10); // The star currently mouse on
        // Now highlight all the stars that's not after the current hovered star
        $(this).parent().children('li.star').each(function (e) {
            if (e < onStar) {
                $(this).addClass('hover');
            }
            else {
                $(this).removeClass('hover');
            }
        });
    }).on('mouseout', function () {
        $(this).parent().children('li.star').each(function (e) {
            $(this).removeClass('hover');
        });
    });
    /* 2. Action to perform on click */
    $('#stars li').on('click', function () {
        var onStar = parseInt($(this).data('value'), 10); // The star currently selected
        var stars = $(this).parent().children('li.star');
        for (var i = 0; i < stars.length; i++) {
            $(stars[i]).removeClass('selected');
        }
        for (var i = 0; i < onStar; i++) {
            $(stars[i]).addClass('selected');
        }
        //POST
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/api/RateRecipe",
            data: "input=" + onStar,
            dataType: "json",
            success: function (data) {
            },
            error: function (result) { }
        });
    });
});
//# sourceMappingURL=searchView.js.map