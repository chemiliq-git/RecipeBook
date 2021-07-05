class АutoCompleteSearch {
    constructor(controlId, searchDataMode) {
        this.controlId = controlId;
        this.searchDataMode = searchDataMode;
    }
    startListenOnKeyUp() {
        $('#' + this.controlId).keyup((event) => {
            let input = $('#' + this.controlId).val().toString();
            let formData = new FormData();
            formData.append(АutoCompleteSearch.AUTOCOMPLETE_SEARCH_INPUT_TEXT, input);
            formData.append(АutoCompleteSearch.AUTOCOMPLETE_SEARCH_DATA_MODE, this.searchDataMode);
            let token = $("#keyForm input[name=__RequestVerificationToken]").val();
            $.ajax({
                url: АutoCompleteSearch.AUTOCOMPLETE_SEARCH_URL,
                data: formData,
                processData: false,
                contentType: false,
                type: Const.POST,
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: (data) => {
                    var availableData = [];
                    data?.forEach((element) => {
                        availableData.push({ id: element.id, label: element.name });
                    });
                    $('#' + this.controlId).autocomplete({
                        source: availableData,
                        minLength: 2,
                        select: function (event, ui) {
                            let value = ui.item.val;
                        }
                    });
                },
                error: function (result) { }
            });
        });
    }
}
АutoCompleteSearch.AUTOCOMPLETE_SEARCH_INPUT_TEXT = "inputText";
АutoCompleteSearch.AUTOCOMPLETE_SEARCH_DATA_MODE = "searchDataMode";
АutoCompleteSearch.AUTOCOMPLETE_SEARCH_URL = "/api/AutocompleteSearch";
//# sourceMappingURL=autocompleteSearch.js.map