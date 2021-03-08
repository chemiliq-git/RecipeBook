$(document).ready(function () {
    let searchViewSideBar = new sideBar();
    searchViewSideBar.init("Ingredient");
    //$('[id^="search_result_items_"]').click(onSearchResultItemClick);
    searchViewSideBar.addEventListener('complete', (e) => {
        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let data = e.detail.formData;
        let modelData = document.getElementById("model_data").getAttribute('value');
        let jsonViewData = '';
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
    });
});
let vData = '';
function onRemoveIngredientsSetItemsClick(id, modeldata) {
    //let idDividerIndex = (<HTMLInputElement>event.target).id.lastIndexOf('_')
    //let selectedId = (<HTMLInputElement>event.target).id.substr(idDividerIndex + 1);
    let selectedId = id;
    let token = $("#keyForm input[name=__RequestVerificationToken]").val();
    let jsonViewData = '';
    if (vData == '') {
        jsonViewData = JSON.stringify(modeldata);
    }
    else {
        jsonViewData = JSON.stringify(vData);
    }
    let data = new FormData();
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
            $('#IngredientsSetItemsListPartialView').html(result.partialView);
            //$('[id^="remove_ingredientsset_items_"]').click(onRemoveIngredientsSetItemsClick);
            //$('#partialViewName').html(result.partialView);
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
    let selectedId = id;
    let token = $("#keyForm input[name=__RequestVerificationToken]").val();
    let jsonViewData = '';
    if (vData == '') {
        jsonViewData = JSON.stringify(modeldata);
    }
    else {
        jsonViewData = JSON.stringify(vData);
    }
    let data = new FormData();
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
            $('#IngredientsSetItemsListPartialView').html(result.partialView);
            //$('[id^="remove_ingredientsset_items_"]').click(onRemoveIngredientsSetItemsClick);
            //$('#partialViewName').html(result.partialView);
            vData = result.data;
        },
        error: function (result) {
            //TODO 
            var error = result;
        }
    });
}
//# sourceMappingURL=ingredientsSetView.js.map