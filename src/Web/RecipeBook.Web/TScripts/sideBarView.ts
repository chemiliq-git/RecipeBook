class sideBar {

    onSideBarChange: any;
    searchDataMode: string;

    constructor(onSideBarChange: any, searchDataMode) {
        let myAutoCompleteSearch = new autoCompleteSearch("#searchBox", searchDataMode);
        myAutoCompleteSearch.startListenOnKeyUp();
        this.onSideBarChange = onSideBarChange;
        this.searchDataMode = searchDataMode;
    }

    startListenOnSideBarChange() {
        var context = this;
        $('input[type="text"]').change(function (event) { context.search(event, context); });
        $('input[type="checkbox"]').change(function (event) { context.search(event, context); });
    }

    search(e, context): any {
        let currentElement = <HTMLInputElement>e.target
        if (currentElement.name.indexOf('Ingr_Type_checkbox_') == 0 && !currentElement.checked) {
            $("input[name^='Ingr_checkbox']").prop("checked", false)
        }

        var formData = new FormData();

        let text = $('input[type="text"]').prop("value");
        formData.append("Text", text);

        let checkedElements = $("input[name^='Recipe_Type_Checkbox']:checked")
        let checkedElementsIds = '';
        if (checkedElements.length > 0) {

            for (var i = 0; i < checkedElements.length; i++) {
                let vCheckedElements = <HTMLInputElement>checkedElements[i];
                checkedElementsIds += vCheckedElements.id + ','
            }

            checkedElementsIds = checkedElementsIds.substr(0, checkedElementsIds.length - 1);
        }
        formData.append("RecipeTypes", checkedElementsIds);


        checkedElements = $("input[name^='Ingr_Checkbox']:checked")
        checkedElementsIds = '';
        if (checkedElements.length > 0) {

            for (var i = 0; i < checkedElements.length; i++) {
                let vCheckedElements = <HTMLInputElement>checkedElements[i];
                checkedElementsIds += vCheckedElements.id + ','
            }

            checkedElementsIds = checkedElementsIds.substr(0, checkedElementsIds.length - 1);
        }
        formData.append("Ingredients", checkedElementsIds);

        context.onSideBarChange(formData);
    };
}