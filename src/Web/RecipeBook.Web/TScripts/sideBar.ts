class sideBar extends EventTarget {
   
    init(searchDataMode: string) {
        this.startListenOnSideBarChange();
        let myAutoCompleteSearch = new АutoCompleteSearch("#searchBox", searchDataMode);
        myAutoCompleteSearch.startListenOnKeyUp();
    }

    startListenOnSideBarChange() {
        $('input[type="text"]').change((event) => { this.search(event); });
        $('input[type="checkbox"]').change((event) => { this.search(event); });
    }

    search(e: JQuery.ChangeEvent): any {
        let currentElement = <HTMLInputElement>e.target
        let ingrs = $(currentElement).parent().attr('aria-controls');  
        let allInpuCheckBoxes = $("input[name^='Ingr_Checkbox']").toArray();

        if (currentElement.name.indexOf('Ingr_Type_Checkbox_') == 0 && currentElement.checked) {              
            
            allInpuCheckBoxes.forEach(function (Element) {
                let element = <HTMLInputElement>Element
                if (ingrs.indexOf(element.id) >= 0) {
                    element.checked =  true;
                }
            });
            
            //$("input[name^='Ingr_Checkbox']").prop("checked", true)
        }
        else if (currentElement.name.indexOf('Ingr_Type_Checkbox_') == 0 && !currentElement.checked) {
            //$("input[name^='Ingr_Checkbox']").prop("checked", false)
            allInpuCheckBoxes.forEach(function (Element) {
                let element = <HTMLInputElement>Element
                if (ingrs.indexOf(element.id) >= 0) {
                    //$(Element).prop("checked", false);
                    element.checked = false;
                }
            });
        } 

        let formData = new FormData();

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

        this.dispatchEvent(new CustomEvent('complete', { detail: { formData: formData } }));
    };
}