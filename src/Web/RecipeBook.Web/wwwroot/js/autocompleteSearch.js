class АutoCompleteSearch {
    constructor(controlName, searchDataMode) {
        this.controlName = controlName;
        this.searchDataMode = searchDataMode;
    }
    startListenOnKeyUp() {
        let context = this;
        $(this.controlName).keyup(function (event) {
            let input = $(context.controlName).val().toString();
            let formData = new FormData();
            formData.append("inputText", input);
            formData.append("searchDataMode", context.searchDataMode);
            let token = $("#keyForm input[name=__RequestVerificationToken]").val();
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
                            let value = ui.item.val;
                        }
                    });
                },
                error: function (result) { }
            });
        });
    }
}
//# sourceMappingURL=autocompleteSearch.js.map