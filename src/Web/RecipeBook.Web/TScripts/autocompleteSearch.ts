interface ResultData {
    id: string;
    name: string;
}

class АutoCompleteSearch {
    private static readonly AUTOCOMPLETE_SEARCH_INPUT_TEXT: string = "inputText";
    private static readonly AUTOCOMPLETE_SEARCH_DATA_MODE: string = "searchDataMode";
    private static readonly AUTOCOMPLETE_SEARCH_URL: string = "/api/AutocompleteSearch";

    private controlId: string;
    private searchDataMode: string;

    constructor(controlId: string, searchDataMode: string) {        
        this.controlId = controlId;
        this.searchDataMode = searchDataMode;
    }

    startListenOnKeyUp(this: АutoCompleteSearch) {
        let context: АutoCompleteSearch = this;

        $('#'+ this.controlId).keyup(function (event) {
            let input = $('#' + context.controlId).val().toString();

            let formData = new FormData();
            formData.append(АutoCompleteSearch.AUTOCOMPLETE_SEARCH_INPUT_TEXT, input);
            formData.append(АutoCompleteSearch.AUTOCOMPLETE_SEARCH_DATA_MODE, context.searchDataMode);
            let token = $("#keyForm input[name=__RequestVerificationToken]").val();
            
            $.ajax(
                {
                    url: АutoCompleteSearch.AUTOCOMPLETE_SEARCH_URL,
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: Const.POST,
                    headers: { 'X-CSRF-TOKEN' : token.toString() },

                    success: function (data: Array<ResultData>) {
                        var availableData = [];
                        data?.forEach((element) => {
                            availableData.push({ id: element.id, label: element.name });
                        });

                        $('#' + context.controlId).autocomplete({
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