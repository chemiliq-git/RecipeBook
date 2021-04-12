$(document).ready(
    function () {
        let myAutoCompleteSearch = new autoCompleteSearch("#searchBox", "Recipe");
        myAutoCompleteSearch.startListenOnKeyUp();

    });