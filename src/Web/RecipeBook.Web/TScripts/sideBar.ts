class SideBar extends EventTarget {
    private static readonly INGR_CHECKBOX_NAME = 'Ingr_Checkbox';
    private static readonly INGR_TYPE_CHECKBOX_NAME = 'Ingr_Type_Checkbox_';
    private static readonly RECIPE_TYPE_CHECKBOX_NAME = 'Recipe_Type_Checkbox_';

    init(searchDataMode: string) {
        this.startListenOnSideBarChange();
        let myAutoCompleteSearch = new АutoCompleteSearch(Const.AUTOCOMLETE_SEARCH_BOX_ELEMENT_ID, searchDataMode);
        myAutoCompleteSearch.startListenOnKeyUp();
    }

    startListenOnSideBarChange() {
        $('input[type="text"]').change((event) => { this.search(event); });
        $('input[type="checkbox"]').change((event) => { this.search(event); });
    }

    search(e: JQuery.ChangeEvent): any {
        let currentElement = <HTMLInputElement>e.target
        let ingrs = $(currentElement).parent().attr('aria-controls');  
        let allInpuCheckBoxes = $("input[name^=" + SideBar.INGR_CHECKBOX_NAME + "]").toArray();

        if (currentElement.name.indexOf(SideBar.INGR_TYPE_CHECKBOX_NAME) == 0 && currentElement.checked) {              
            
            allInpuCheckBoxes.forEach(function (Element) {
                let element = <HTMLInputElement>Element
                if (ingrs.indexOf(element.id) >= 0) {
                    element.checked =  true;
                }
            });
            
            //$("input[name^='Ingr_Checkbox']").prop("checked", true)
        }
        else if (currentElement.name.indexOf(SideBar.INGR_TYPE_CHECKBOX_NAME) == 0 && !currentElement.checked) {
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
        formData.append(SideBarSearchFieldEnum.Text, text);

        let checkedElements = $("input[name^=" + SideBar.RECIPE_TYPE_CHECKBOX_NAME +"]:checked")
        let checkedElementsIds = '';
        if (checkedElements.length > 0) {

            for (var i = 0; i < checkedElements.length; i++) {
                let vCheckedElements = <HTMLInputElement>checkedElements[i];
                checkedElementsIds += vCheckedElements.id + ','
            }

            checkedElementsIds = checkedElementsIds.substr(0, checkedElementsIds.length - 1);
        }
        formData.append(SideBarSearchFieldEnum.RecipeTypes, checkedElementsIds);


        checkedElements = $("input[name^=" + SideBar.INGR_CHECKBOX_NAME + "]:checked")
        checkedElementsIds = '';
        if (checkedElements.length > 0) {

            for (var i = 0; i < checkedElements.length; i++) {
                let vCheckedElements = <HTMLInputElement>checkedElements[i];
                checkedElementsIds += vCheckedElements.id + ','
            }

            checkedElementsIds = checkedElementsIds.substr(0, checkedElementsIds.length - 1);
        }
        formData.append(SideBarSearchFieldEnum.Ingredients, checkedElementsIds);

        this.dispatchEvent(new CustomEvent(Const.SIDE_BAR_COMPLETE_EVENT_NAME, { detail: { formData: formData } }));
    };
}