class autoCompleteSearch {
    constructor(controlName, searchDataMode) {
        this.controlName = controlName;
        this.searchDataMode = searchDataMode;
    }
    startListenOnKeyUp() {
        let context = this;
        $(context.controlName).keyup(function (event) {
            let input = $(context.controlName).val().toString();
            var formData = new FormData();
            formData.append("inputText", input);
            formData.append("searchDataMode", context.searchDataMode);
            var token = $("#keyForm input[name=__RequestVerificationToken]").val();
            $.ajax({
                url: "/api/AutocompleteSearch",
                data: formData,
                processData: false,
                contentType: false,
                type: "POST",
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: function (data) {
                    var availableData = [];
                    data.forEach((element) => {
                        availableData.push({ id: element.id, label: element.name });
                    });
                    $(context.controlName).autocomplete({
                        source: availableData,
                        minLength: 2,
                        select: function (event, ui) {
                            var value = ui.item.val;
                        }
                    });
                },
                error: function (result) { }
            });
        });
    }
}
//# sourceMappingURL=autocompleteSearch.js.map