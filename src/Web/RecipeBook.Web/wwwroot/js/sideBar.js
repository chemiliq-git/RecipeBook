class sideBar extends EventTarget {
    init(searchDataMode) {
        this.startListenOnSideBarChange();
        let myAutoCompleteSearch = new autoCompleteSearch("#searchBox", searchDataMode);
        myAutoCompleteSearch.startListenOnKeyUp();
    }
    startListenOnSideBarChange() {
        $('input[type="text"]').change((event) => { this.search(event); });
        $('input[type="checkbox"]').change((event) => { this.search(event); });
    }
    search(e) {
        let currentElement = e.target;
        if (currentElement.name.indexOf('Ingr_Type_checkbox_') == 0 && !currentElement.checked) {
            $("input[name^='Ingr_checkbox']").prop("checked", false);
        }
        var formData = new FormData();
        let text = $('input[type="text"]').prop("value");
        formData.append("Text", text);
        let checkedElements = $("input[name^='Recipe_Type_Checkbox']:checked");
        let checkedElementsIds = '';
        if (checkedElements.length > 0) {
            for (var i = 0; i < checkedElements.length; i++) {
                let vCheckedElements = checkedElements[i];
                checkedElementsIds += vCheckedElements.id + ',';
            }
            checkedElementsIds = checkedElementsIds.substr(0, checkedElementsIds.length - 1);
        }
        formData.append("RecipeTypes", checkedElementsIds);
        checkedElements = $("input[name^='Ingr_Checkbox']:checked");
        checkedElementsIds = '';
        if (checkedElements.length > 0) {
            for (var i = 0; i < checkedElements.length; i++) {
                let vCheckedElements = checkedElements[i];
                checkedElementsIds += vCheckedElements.id + ',';
            }
            checkedElementsIds = checkedElementsIds.substr(0, checkedElementsIds.length - 1);
        }
        formData.append("Ingredients", checkedElementsIds);
        this.dispatchEvent(new CustomEvent('complete', { detail: { formData: formData } }));
    }
    ;
}
//# sourceMappingURL=sideBar.js.map