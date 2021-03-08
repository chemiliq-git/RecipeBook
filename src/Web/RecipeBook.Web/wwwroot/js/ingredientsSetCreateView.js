$(document).ready(function () {
    var searchViewSideBar = new sideBar(onSideBarChange, "Ingredient");
    searchViewSideBar.startListenOnSideBarChange();
    //$('[id^="search_result_items_"]').click(onSearchResultItemClick);
    function onSideBarChange(data) {
        var token = $("#keyForm input[name=__RequestVerificationToken]").val();
        var modelData = document.getElementById("model_data").getAttribute('value');
        var jsonViewData = '';
        if (vData == '') {
            jsonViewData = /*JSON.stringify(*/ modelData /*)*/;
        }
        else {
            jsonViewData = JSON.stringify(vData);
        }
        data.append("modelData", jsonViewData);
        $.ajax({
            url: "/IngredientsSet/SideBarSearch",
            data: data,
            processData: false,
            contentType: false,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: function (result) {
                $('#SearchResultListPartialView').html(result.partialView);
                //$('[id^="search_result_items_"]').click(onSearchResultItemClick);
                vData = result.data;
            },
            error: function (result) {
                //TODO 
                var error = result;
            }
        });
    }
});
var vData = '';
function onRemoveIngredientsSetItemsClick(id, modeldata) {
    //let idDividerIndex = (<HTMLInputElement>event.target).id.lastIndexOf('_')
    //let selectedId = (<HTMLInputElement>event.target).id.substr(idDividerIndex + 1);
    var selectedId = id;
    var token = $("#keyForm input[name=__RequestVerificationToken]").val();
    var jsonViewData = '';
    if (vData == '') {
        jsonViewData = JSON.stringify(modeldata);
    }
    else {
        jsonViewData = JSON.stringify(vData);
    }
    var data = new FormData();
    data.append("selectedId", selectedId);
    data.append("modelData", jsonViewData);
    $.ajax({
        url: "/IngredientsSet/Remove",
        data: data,
        processData: false,
        contentType: false,
        type: "POST",
        headers: { 'X-CSRF-TOKEN': token.toString() },
        success: function (result) {
            $('#CreateIngredientsSetItemsListPartialView').html(result.partialView);
            //$('[id^="remove_ingredientsset_items_"]').click(onRemoveIngredientsSetItemsClick);
            vData = result.data;
        },
        error: function (result) {
            //TODO 
            var error = result;
        }
    });
}
function onSearchResultItemClick(id, modeldata) {
    //let idDividerIndex = (<HTMLInputElement>event.target).id.lastIndexOf('_');
    //let selectedId = (<HTMLInputElement>event.target).id.substr(idDividerIndex + 1);
    var selectedId = id;
    var token = $("#keyForm input[name=__RequestVerificationToken]").val();
    var jsonViewData = '';
    if (vData == '') {
        jsonViewData = JSON.stringify(modeldata);
    }
    else {
        jsonViewData = JSON.stringify(vData);
    }
    var data = new FormData();
    data.append("selectedId", selectedId);
    data.append("modelData", jsonViewData);
    $.ajax({
        url: "/IngredientsSet/Add",
        data: data,
        processData: false,
        contentType: false,
        type: "POST",
        headers: { 'X-CSRF-TOKEN': token.toString() },
        success: function (result) {
            $('#CreateIngredientsSetItemsListPartialView').html(result.partialView);
            //$('[id^="remove_ingredientsset_items_"]').click(onRemoveIngredientsSetItemsClick);
            vData = result.data;
        },
        error: function (result) {
            //TODO 
            var error = result;
        }
    });
}
//# sourceMappingURL=ingredientsSetCreateView.js.map