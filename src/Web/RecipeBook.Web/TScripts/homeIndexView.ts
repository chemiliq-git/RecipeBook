$(document).ready(
    function () {
        let myAutoCompleteSearch = new АutoCompleteSearch("#searchBox", AutoCompleteSearchModeEnum.Recipe.toString());
        myAutoCompleteSearch.startListenOnKeyUp();

    });