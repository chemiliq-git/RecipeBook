interface ResultData {
    id: string;
    name: string;
}

class autoCompleteSearch {

    controlName: string;

    constructor(controlName: string) {        
        this.controlName = controlName;
    }

    startListenOnKeyUp() {
        let context: autoCompleteSearch = this;

        $(context.controlName).keyup(function (event) {
            let input = $(context.controlName).val().toString();

            var formData = new FormData();
            formData.append("inputText", input);
            var token = $("#keyForm input[name=__RequestVerificationToken]").val();

            $.ajax(
                {
                    url: "/api/AutocompleteSearch",
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: "POST",
                    headers: { 'X-CSRF-TOKEN': token.toString() },

                    success: function (data: Array<ResultData>) {
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