$('input[type="checkbox"]').change(function (e) {
    let currentElement = <HTMLInputElement>e.target
    if (currentElement.id.indexOf('Ingr_Type_checkbox_ ') == 0 && !currentElement.checked) {
        $("input[id^='Ingr_checkbox']").prop("checked", false)
    }


    let checkedElements = $("input[id^='Ingr_checkbox']:checked")
    let checkedElementsNames = '';
    if (checkedElements.length > 0) {

        for (var i = 0; i < checkedElements.length; i++) {
            let vCheckedElements = <HTMLInputElement>checkedElements[i];
            checkedElementsNames += vCheckedElements.name + ','
        }

        checkedElementsNames = checkedElementsNames.substr(0, checkedElementsNames.length - 1);
    }
    var url = '@Url.Action("SearchPartial", "Home")';
    var formData = new FormData();
    var data = checkedElementsNames;
    formData.append("inputText", data);
    var token = $("#keyForm input[name=__RequestVerificationToken]").val();
    $.ajax({
        url: url,
        data: formData,
        processData: false,
        contentType: false,
        type: "POST",
        headers: { 'X-CSRF-TOKEN': token.toString() },
        success: function (view) {
            $('#partialView').html(view)
        },
        error: function (result) {
            var error = result;
        }
    });

});

interface ResultData {
    id: string;
    name: string;
}

$('#searchresult').keyup(function (event) {
    let input = $('#searchresult').val().toString();

    var url = '@Url.Action("SearchPartial", "Home")';
    var formData = new FormData();    
    formData.append("inputText", input);
    var token = $("#keyForm input[name=__RequestVerificationToken]").val();

    $.ajax(
        {
            //type: "GET",
            //contentType: "application/json; charset=utf-8",
            //url: "/api/LiveSearchs",
            //data: "input=" + input,
            //dataType: "json",
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },

            success: function (data: Array<ResultData>) {
                var availableData = [];
                data.forEach((element) => {
                    availableData.push({ id: element.id, label: element.name });
                });

                $("#searchresult").autocomplete({                    
                    source: availableData,
                    minLength: 2,
                    select: function (event, ui) {
                        var value = ui.item.val;

                        //var sts = "no";
                        //var url = 'Productlist.aspx?prefix=' + ptxt; // ur own conditions
                        //$(location).attr('href', url);
                    }
                });
            },
            error: function (result) { }
        });

    
});

$(document).ready(function () {

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

        for (let i = 0; i < stars.length; i++) {
            $(stars[i]).removeClass('selected');
        }

        for (let i = 0; i < onStar; i++) {
            $(stars[i]).addClass('selected');
        }

        //POST
        $.ajax(
            {
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