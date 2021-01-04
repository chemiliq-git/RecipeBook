var autoCompleteSearch = /** @class */ (function () {
    function autoCompleteSearch(controlName) {
        this.controlName = controlName;
    }
    autoCompleteSearch.prototype.startListenOnKeyUp = function () {
        var context = this;
        $(context.controlName).keyup(function (event) {
            var input = $(context.controlName).val().toString();
            var formData = new FormData();
            formData.append("inputText", input);
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
                    data.forEach(function (element) {
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
    };
    return autoCompleteSearch;
}());
//# sourceMappingURL=autocompleteSearch.js.map