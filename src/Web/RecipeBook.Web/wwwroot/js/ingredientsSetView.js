let vData = '';
const INGREDIENTS_SET_REMOVE_URL = "/IngredientsSet/Remove";
const INGREDIENTS_SET_ADD_URL = "/IngredientsSet/Add";
const INGREDIENTS_SET_ITEMS_LIST_PARTIAL_VIEW_ID = 'IngredientsSetItemsListPartialView';
const INGREDIENTS_ITEM_SELECTED_ID_KEY = 'selectedId';
const INGREDIENTS_SET_ITEM_MODEL_DATA_KEY = 'modelData';
const MODEL_DATA_ELEMENET_ID = 'model_data';
$(document).ready(function () {
    const INGREDIENTS_SET_SIDE_BAR_SEARCH_URL = "/IngredientsSet/SideBarSearch";
    const SEARCH_RESULT_LIST_PARTIAL_VIEW_ID = "SearchResultListPartialView";
    let searchViewSideBar = new SideBar();
    searchViewSideBar.init(AutoCompleteSearchModeEnum.Ingredient.toString());
    //$('[id^="search_result_items_"]').click(onSearchResultItemClick);
    searchViewSideBar.addEventListener(Const.SIDE_BAR_COMPLETE_EVENT_NAME, (e) => {
        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let data = e.detail.formData;
        let modelData = document.getElementById(MODEL_DATA_ELEMENET_ID).getAttribute(Const.HTML_ATTRIBUTE_VALUE_KEY);
        let jsonViewData = '';
        if (vData == '') {
            jsonViewData = /*JSON.stringify(*/ modelData /*)*/;
        }
        else {
            jsonViewData = JSON.stringify(vData);
        }
        data.append(INGREDIENTS_SET_ITEM_MODEL_DATA_KEY, jsonViewData);
        $.ajax({
            url: INGREDIENTS_SET_SIDE_BAR_SEARCH_URL,
            data: data,
            processData: false,
            contentType: false,
            type: Const.POST,
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: function (result) {
                $('#' + SEARCH_RESULT_LIST_PARTIAL_VIEW_ID).html(result.partialView);
                //$('[id^="search_result_items_"]').click(onSearchResultItemClick);
                vData = result.data;
            },
            error: function (result) {
                //TODO 
                let error = result;
            }
        });
    });
});
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
    data.append(INGREDIENTS_ITEM_SELECTED_ID_KEY, selectedId);
    data.append(INGREDIENTS_SET_ITEM_MODEL_DATA_KEY, jsonViewData);
    $.ajax({
        url: INGREDIENTS_SET_REMOVE_URL,
        data: data,
        processData: false,
        contentType: false,
        type: Const.POST,
        headers: { 'X-CSRF-TOKEN': token.toString() },
        success: function (result) {
            $('#' + INGREDIENTS_SET_ITEMS_LIST_PARTIAL_VIEW_ID).html(result.partialView);
            //$('[id^="remove_ingredientsset_items_"]').click(onRemoveIngredientsSetItemsClick);
            //$('#partialViewName').html(result.partialView);
            vData = result.data;
        },
        error: function (result) {
            //TODO 
            let error = result;
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
    data.append(INGREDIENTS_ITEM_SELECTED_ID_KEY, selectedId);
    data.append(INGREDIENTS_SET_ITEM_MODEL_DATA_KEY, jsonViewData);
    $.ajax({
        url: INGREDIENTS_SET_ADD_URL,
        data: data,
        processData: false,
        contentType: false,
        type: Const.POST,
        headers: { 'X-CSRF-TOKEN': token.toString() },
        success: function (result) {
            $('#' + INGREDIENTS_SET_ITEMS_LIST_PARTIAL_VIEW_ID).html(result.partialView);
            //$('[id^="remove_ingredientsset_items_"]').click(onRemoveIngredientsSetItemsClick);
            //$('#partialViewName').html(result.partialView);
            vData = result.data;
        },
        error: function (result) {
            //TODO 
            let error = result;
        }
    });
}
//# sourceMappingURL=ingredientsSetView.js.map