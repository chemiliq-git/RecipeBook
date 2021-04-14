class RecipeIndexViewHelper {
    constructor(searchText, searchRecipeTypes, searchIngredients) {
        this.onHomeVoteSuccess = () => {
            let token = $("#keyForm input[name=__RequestVerificationToken]").val();
            let data = new FormData();
            data.append(SideBarSearchFieldEnum.Text, this.searchText);
            data.append(SideBarSearchFieldEnum.RecipeTypes, this.searchRecipeTypes);
            data.append(SideBarSearchFieldEnum.Ingredients, this.searchIngredients);
            $.ajax({
                url: RecipeIndexViewHelper.RECIPE_SIDEBAR_SEARCH_URL,
                data: data,
                processData: false,
                contentType: false,
                type: Const.POST,
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: (result) => {
                    $('#' + RecipeIndexViewHelper.PARTIAL_VIEW_ID).html(result);
                    let fTasteStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_TASTE_RATE_ELEMENT_ID);
                    fTasteStarsVote.addEventListener(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME, this.onHomeVoteSuccess);
                    fTasteStarsVote.startListenToVote();
                    let fEasyStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_EASY_RATE_ELEMENT_ID);
                    fEasyStarsVote.addEventListener(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME, this.onHomeVoteSuccess);
                    fEasyStarsVote.startListenToVote();
                },
                error: function (error) {
                    if (error.status == Const.NO_AUTHENTICATION_ERROR_NUMBER) {
                        window.location.href = Const.IDENTITY_LOGIN_URL;
                    }
                    else {
                        //TODO show custom error msg
                    }
                }
            });
        };
        this.onSideBarComplete = (e) => {
            let token = $("#keyForm input[name=__RequestVerificationToken]").val();
            let data = e.detail.formData;
            this.searchText = data.get(SideBarSearchFieldEnum.Text).toString();
            this.searchRecipeTypes = data.get(SideBarSearchFieldEnum.RecipeTypes).toString();
            this.searchIngredients = data.get(SideBarSearchFieldEnum.Ingredients).toString();
            $.ajax({
                url: RecipeIndexViewHelper.RECIPE_SIDEBAR_SEARCH_URL,
                data: data,
                processData: false,
                contentType: false,
                type: Const.POST,
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: (result) => {
                    $('#' + RecipeIndexViewHelper.PARTIAL_VIEW_ID).html(result);
                    let fTasteStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_TASTE_RATE_ELEMENT_ID);
                    fTasteStarsVote.addEventListener(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME, this.onHomeVoteSuccess);
                    fTasteStarsVote.startListenToVote();
                    let fEasyStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_EASY_RATE_ELEMENT_ID);
                    fEasyStarsVote.addEventListener(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME, this.onHomeVoteSuccess);
                    fEasyStarsVote.startListenToVote();
                },
                error: function (result) {
                    //TODO 
                    let error = result;
                }
            });
        };
        this.searchText = searchText;
        this.searchRecipeTypes = searchRecipeTypes;
        this.searchIngredients = searchIngredients;
        let searchViewSideBar = new SideBar();
        searchViewSideBar.init(AutoCompleteSearchModeEnum.Recipe.toString());
        searchViewSideBar.addEventListener(Const.SIDE_BAR_COMPLETE_EVENT_NAME, this.onSideBarComplete);
        let fTasteStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_TASTE_RATE_ELEMENT_ID);
        fTasteStarsVote.addEventListener(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME, this.onHomeVoteSuccess);
        fTasteStarsVote.startListenToVote();
        let fEasyStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_EASY_RATE_ELEMENT_ID);
        fEasyStarsVote.addEventListener(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME, this.onHomeVoteSuccess);
        fEasyStarsVote.startListenToVote();
    }
    OnMenuClick(id) {
        let isAdded = $(document.getElementById('MenuButton_' + id)).data(Const.HTML_ATTRIBUTE_VALUE_KEY);
        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let data = new FormData();
        data.append("id", id);
        if (isAdded) {
            $.ajax({
                url: RecipeIndexViewHelper.RECIPE_SIDEBAR_ADD_TO_MENU_URL,
                processData: false,
                contentType: false,
                data: data,
                type: Const.POST,
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: function (result) {
                    let btnElement = document.getElementById('MenuButton_' + result.id);
                    btnElement.setAttribute("class", "btn btn-sm btn-danger rounded-pill");
                    $(btnElement).data(Const.HTML_ATTRIBUTE_VALUE_KEY, false);
                    document.getElementById('MenuButton_' + result.id).children[0].setAttribute("class", "fas fa-minus-circle");
                },
                error: function (error) {
                    if (error.status == Const.NO_AUTHENTICATION_ERROR_NUMBER) {
                        window.location.href = Const.IDENTITY_LOGIN_URL;
                    }
                    else {
                        //TODO show custom error msg
                    }
                }
            });
        }
        else {
            $.ajax({
                url: RecipeIndexViewHelper.RECIPE_SIDEBAR_REMOVE_FROM_MENU_URL,
                processData: false,
                contentType: false,
                data: data,
                type: Const.POST,
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: function (result) {
                    let btnElement = document.getElementById('MenuButton_' + result.id);
                    btnElement.setAttribute("class", "btn btn-sm btn-success rounded-pill");
                    $(btnElement).data(Const.HTML_ATTRIBUTE_VALUE_KEY, true);
                    document.getElementById('MenuButton_' + result.id).children[0].setAttribute("class", "fas fa-plus-circle");
                },
                error: function (error) {
                    if (error.status == Const.NO_AUTHENTICATION_ERROR_NUMBER) {
                        window.location.href = Const.IDENTITY_LOGIN_URL;
                    }
                    else {
                        //TODO show custom error msg
                    }
                }
            });
        }
    }
    OnCookedTodayClick(id) {
        let selectedId = id;
        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let data = new FormData();
        data.append("id", selectedId);
        data.append(SideBarSearchFieldEnum.Text, this.searchText);
        data.append(SideBarSearchFieldEnum.RecipeTypes, this.searchRecipeTypes);
        data.append(SideBarSearchFieldEnum.Ingredients, this.searchIngredients);
        $.ajax({
            url: Const.RECIPE_SIDEBAR_UPADTE_LAST_COOKED_URL,
            data: data,
            processData: false,
            contentType: false,
            type: Const.POST,
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: (result) => {
                if (result.result != '') {
                    let token = $("#keyForm input[name=__RequestVerificationToken]").val();
                    let data = new FormData();
                    data.append(SideBarSearchFieldEnum.Text, this.searchText);
                    data.append(SideBarSearchFieldEnum.RecipeTypes, this.searchRecipeTypes);
                    data.append(SideBarSearchFieldEnum.Ingredients, this.searchIngredients);
                    $.ajax({
                        url: RecipeIndexViewHelper.RECIPE_SIDEBAR_SEARCH_URL,
                        data: data,
                        processData: false,
                        contentType: false,
                        type: Const.POST,
                        headers: { 'X-CSRF-TOKEN': token.toString() },
                        success: (result) => {
                            $('#' + RecipeIndexViewHelper.PARTIAL_VIEW_ID).html(result);
                            let fTasteStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_TASTE_RATE_ELEMENT_ID);
                            fTasteStarsVote.addEventListener(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME, this.onHomeVoteSuccess);
                            fTasteStarsVote.startListenToVote();
                            let fEasyStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_EASY_RATE_ELEMENT_ID);
                            fEasyStarsVote.addEventListener(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME, this.onHomeVoteSuccess);
                            fEasyStarsVote.startListenToVote();
                        }
                    });
                }
            },
            error: function (error) {
                if (error.status == Const.NO_AUTHENTICATION_ERROR_NUMBER) {
                    window.location.href = Const.IDENTITY_LOGIN_URL;
                }
                else {
                    //TODO show custom error msg
                }
            }
        });
    }
    OnDelete(id, name) {
        if (confirm('Are you sure you want ot delete ' + name + ' recipe ?')) {
            let selectedId = id;
            let token = $("#keyForm input[name=__RequestVerificationToken]").val();
            let data = new FormData();
            data.append("id", selectedId);
            $.ajax({
                url: RecipeIndexViewHelper.RECIPE_SIDEBAR_DELETE_URL,
                data: data,
                processData: false,
                contentType: false,
                type: Const.POST,
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: (result) => {
                    let token = $("#keyForm input[name=__RequestVerificationToken]").val();
                    let data = new FormData();
                    data.append(SideBarSearchFieldEnum.Text, this.searchText);
                    data.append(SideBarSearchFieldEnum.RecipeTypes, this.searchRecipeTypes);
                    data.append(SideBarSearchFieldEnum.Ingredients, this.searchIngredients);
                    $.ajax({
                        url: RecipeIndexViewHelper.RECIPE_SIDEBAR_SEARCH_URL,
                        data: data,
                        processData: false,
                        contentType: false,
                        type: Const.POST,
                        headers: { 'X-CSRF-TOKEN': token.toString() },
                        success: (result) => {
                            $('#' + RecipeIndexViewHelper.PARTIAL_VIEW_ID).html(result);
                            let fTasteStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_TASTE_RATE_ELEMENT_ID);
                            fTasteStarsVote.addEventListener(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME, this.onHomeVoteSuccess);
                            fTasteStarsVote.startListenToVote();
                            let fEasyStarsVote = new FiveStarsVote(Const.FIVE_STARS_VOTE_EASY_RATE_ELEMENT_ID);
                            fEasyStarsVote.addEventListener(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME, this.onHomeVoteSuccess);
                            fEasyStarsVote.startListenToVote();
                        }
                    });
                },
                error: function (error) {
                    if (error.status == Const.NO_AUTHENTICATION_ERROR_NUMBER) {
                        window.location.href = Const.IDENTITY_LOGIN_URL;
                    }
                    else {
                        //TODO show custom error msg
                    }
                }
            });
        }
    }
}
RecipeIndexViewHelper.PARTIAL_VIEW_ID = "partialView";
RecipeIndexViewHelper.RECIPE_SIDEBAR_SEARCH_URL = "/Recipe/SideBarSearch";
RecipeIndexViewHelper.RECIPE_SIDEBAR_ADD_TO_MENU_URL = "/Recipe/AddRecipeToMenu";
RecipeIndexViewHelper.RECIPE_SIDEBAR_REMOVE_FROM_MENU_URL = "/Recipe/RemoveRecipeFromMenu";
RecipeIndexViewHelper.RECIPE_SIDEBAR_DELETE_URL = "/Recipe/DeleteRecipe";
//# sourceMappingURL=recipeIndexView.js.map