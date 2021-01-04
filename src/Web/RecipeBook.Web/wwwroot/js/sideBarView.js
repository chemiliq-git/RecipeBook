var sideBar = /** @class */ (function () {
    function sideBar(onSideBarChange) {
        var myAutoCompleteSearch = new autoCompleteSearch("#searchBox");
        myAutoCompleteSearch.startListenOnKeyUp();
        this.onSideBarChange = onSideBarChange;
    }
    sideBar.prototype.startListenOnSideBarChange = function () {
        var context = this;
        $('input[type="text"]').change(function (event) { context.search(event, context); });
        $('input[type="checkbox"]').change(function (event) { context.search(event, context); });
    };
    sideBar.prototype.search = function (e, context) {
        var currentElement = e.target;
        if (currentElement.name.indexOf('Ingr_Type_checkbox_') == 0 && !currentElement.checked) {
            $("input[name^='Ingr_checkbox']").prop("checked", false);
        }
        var formData = new FormData();
        var text = $('input[type="text"]').prop("value");
        formData.append("Text", text);
        var checkedElements = $("input[name^='Recipe_Type_Checkbox']:checked");
        var checkedElementsIds = '';
        if (checkedElements.length > 0) {
            for (var i = 0; i < checkedElements.length; i++) {
                var vCheckedElements = checkedElements[i];
                checkedElementsIds += vCheckedElements.id + ',';
            }
            checkedElementsIds = checkedElementsIds.substr(0, checkedElementsIds.length - 1);
        }
        formData.append("RecipeTypes", checkedElementsIds);
        checkedElements = $("input[name^='Ingr_Checkbox']:checked");
        checkedElementsIds = '';
        if (checkedElements.length > 0) {
            for (var i = 0; i < checkedElements.length; i++) {
                var vCheckedElements = checkedElements[i];
                checkedElementsIds += vCheckedElements.id + ',';
            }
            checkedElementsIds = checkedElementsIds.substr(0, checkedElementsIds.length - 1);
        }
        formData.append("Ingredients", checkedElementsIds);
        context.onSideBarChange(formData);
    };
    ;
    return sideBar;
}());
//# sourceMappingURL=sideBarView.js.map