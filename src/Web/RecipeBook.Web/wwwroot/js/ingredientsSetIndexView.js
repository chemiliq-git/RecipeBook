$(document).ready(function () {
    var vData = '';
    var searchViewSideBar = new sideBar(onSideBarChange);
    searchViewSideBar.startListenOnSideBarChange();
    var context = this;
    $('[id^="search_result_items_"]').click(onSearchResultItemClick);
    function onSideBarChange(data) {
        var token = $("#keyForm input[name=__RequestVerificationToken]").val();
        $.ajax({
            url: "/IngredientsSet/SideBarSearch",
            data: data,
            processData: false,
            contentType: false,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: function (result) {
                $('#SearchResultListPartialView').html(result);
            },
            error: function (result) {
                //TODO 
                var error = result;
            }
        });
    }
    function onRemoveIngredientsSetItemsClick(event) {
        var idDividerIndex = event.target.id.lastIndexOf('_');
        var selectedId = event.target.id.substr(idDividerIndex + 1);
        var token = $("#keyForm input[name=__RequestVerificationToken]").val();
        var jsonViewData = '';
        if (vData != '') {
            jsonViewData = JSON.stringify(vData);
        }
        var data = new FormData();
        data.append("selectedId", selectedId);
        data.append("data", jsonViewData);
        $.ajax({
            url: "/IngredientsSet/Remove",
            data: data,
            processData: false,
            contentType: false,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: function (result) {
                $('#CreateIngredientsSetItemsListPartialView').html(result.partialView);
                $('[id^="remove_ingredientsset_items_"]').click(onRemoveIngredientsSetItemsClick);
                vData = result.data;
            },
            error: function (result) {
                //TODO 
                var error = result;
            }
        });
    }
    function onSearchResultItemClick(event) {
        var idDividerIndex = event.target.id.lastIndexOf('_');
        var selectedId = event.target.id.substr(idDividerIndex + 1);
        var token = $("#keyForm input[name=__RequestVerificationToken]").val();
        var jsonViewData = '';
        if (vData != '') {
            jsonViewData = JSON.stringify(vData);
        }
        var data = new FormData();
        data.append("selectedId", selectedId);
        data.append("data", jsonViewData);
        $.ajax({
            url: "/IngredientsSet/Add",
            data: data,
            processData: false,
            contentType: false,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: function (result) {
                $('#CreateIngredientsSetItemsListPartialView').html(result.partialView);
                $('[id^="remove_ingredientsset_items_"]').click(onRemoveIngredientsSetItemsClick);
                vData = result.data;
            },
            error: function (result) {
                //TODO 
                var error = result;
            }
        });
    }
});
//# sourceMappingURL=ingredientsSetIndexView.js.map