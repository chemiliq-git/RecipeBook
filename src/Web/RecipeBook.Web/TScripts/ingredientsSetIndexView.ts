$(document).ready(function () {
    let vData: string = '';
    let searchViewSideBar = new sideBar(onSideBarChange);
    searchViewSideBar.startListenOnSideBarChange();
    let context = this;
    $('[id^="search_result_items_"]').click(onSearchResultItemClick);


    function onSideBarChange(data: any) {

        let token = $("#keyForm input[name=__RequestVerificationToken]").val();

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
        let idDividerIndex = (<HTMLInputElement>event.target).id.lastIndexOf('_')
        let selectedId = (<HTMLInputElement>event.target).id.substr(idDividerIndex + 1);
       
        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let jsonViewData = '';
        if (vData != '') {
            jsonViewData = JSON.stringify(vData);
        }
        let data = new FormData();
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
                $('#IngredientsSetItemsListPartialView').html(result.partialView);
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
        let idDividerIndex = (<HTMLInputElement>event.target).id.lastIndexOf('_');
        let selectedId = (<HTMLInputElement>event.target).id.substr(idDividerIndex + 1);
        
        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let jsonViewData = '';
        if (vData != '') {
            jsonViewData = JSON.stringify(vData);
        }
        let data = new FormData();
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
                $('#IngredientsSetItemsListPartialView').html(result.partialView);
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