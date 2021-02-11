$(document).ready(
    function () {
        var myAutoCompleteSearch = new autoCompleteSearch("#searchBox", "Recipe");
        myAutoCompleteSearch.startListenOnKeyUp();

    });