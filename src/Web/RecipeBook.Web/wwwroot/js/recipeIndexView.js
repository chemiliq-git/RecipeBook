class RecipeIndexViewHelper {
    constructor(searchText, searchRecipeTypes, searchIngredients) {
        this.onHomeVoteSuccess = () => {
            let token = $("#keyForm input[name=__RequestVerificationToken]").val();
            let data = new FormData();
            data.append("Text", this.searchText);
            data.append("RecipeTypes", this.searchRecipeTypes);
            data.append("Ingredients", this.searchIngredients);
            $.ajax({
                url: "/Recipe/SideBarSearch",
                data: data,
                processData: false,
                contentType: false,
                type: "POST",
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: (result) => {
                    $('#partialView').html(result);
                    let fTasteStarsVote = new FiveStarsVote("TasteRateStars");
                    fTasteStarsVote.addEventListener('voteSuccess', this.onHomeVoteSuccess);
                    fTasteStarsVote.startListenToVote();
                    let fEasyStarsVote = new FiveStarsVote("EasyRateStars");
                    fEasyStarsVote.addEventListener('voteSuccess', this.onHomeVoteSuccess);
                    fEasyStarsVote.startListenToVote();
                },
                error: function (error) {
                    if (error.status == 401) {
                        window.location.href = '/Identity/Account/Login';
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
            this.searchText = data.get("Text").toString();
            this.searchRecipeTypes = data.get("RecipeTypes").toString();
            this.searchIngredients = data.get("Ingredients").toString();
            $.ajax({
                url: "/Recipe/SideBarSearch",
                data: data,
                processData: false,
                contentType: false,
                type: "POST",
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: (result) => {
                    $('#partialView').html(result);
                    let fTasteStarsVote = new FiveStarsVote("TasteRateStars");
                    fTasteStarsVote.addEventListener('voteSuccess', this.onHomeVoteSuccess);
                    fTasteStarsVote.startListenToVote();
                    let fEasyStarsVote = new FiveStarsVote("EasyRateStars");
                    fEasyStarsVote.addEventListener('voteSuccess', this.onHomeVoteSuccess);
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
        let searchViewSideBar = new sideBar();
        searchViewSideBar.init("Recipe");
        searchViewSideBar.addEventListener('complete', this.onSideBarComplete);
        let fTasteStarsVote = new FiveStarsVote("TasteRateStars");
        fTasteStarsVote.addEventListener('voteSuccess', this.onHomeVoteSuccess);
        fTasteStarsVote.startListenToVote();
        let fEasyStarsVote = new FiveStarsVote("EasyRateStars");
        fEasyStarsVote.addEventListener('voteSuccess', this.onHomeVoteSuccess);
        fEasyStarsVote.startListenToVote();
    }
    OnMenuClick(id) {
        let isAdded = $(document.getElementById('MenuButton_' + id)).data('value');
        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let data = new FormData();
        data.append("id", id);
        if (isAdded) {
            $.ajax({
                url: "/Recipe/AddRecipeToMenu",
                processData: false,
                contentType: false,
                data: data,
                type: "POST",
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: function (result) {
                    let btnElement = document.getElementById('MenuButton_' + result.id);
                    btnElement.setAttribute("class", "btn btn-sm btn-danger rounded-pill");
                    $(btnElement).data('value', false);
                    document.getElementById('MenuButton_' + result.id).children[0].setAttribute("class", "fas fa-minus-circle");
                },
                error: function (error) {
                    if (error.status == 401) {
                        window.location.href = '/Identity/Account/Login';
                    }
                    else {
                        //TODO show custom error msg
                    }
                }
            });
        }
        else {
            $.ajax({
                url: "/Recipe/RemoveRecipeFromMenu",
                processData: false,
                contentType: false,
                data: data,
                type: "POST",
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: function (result) {
                    let btnElement = document.getElementById('MenuButton_' + result.id);
                    btnElement.setAttribute("class", "btn btn-sm btn-success rounded-pill");
                    $(btnElement).data('value', true);
                    document.getElementById('MenuButton_' + result.id).children[0].setAttribute("class", "fas fa-plus-circle");
                },
                error: function (error) {
                    if (error.status == 401) {
                        window.location.href = '/Identity/Account/Login';
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
        data.append("Text", this.searchText);
        data.append("RecipeTypes", this.searchRecipeTypes);
        data.append("Ingredients", this.searchIngredients);
        $.ajax({
            url: "/Recipe/UpdateLastCookedDate",
            data: data,
            processData: false,
            contentType: false,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: (result) => {
                if (result.result != '') {
                    let token = $("#keyForm input[name=__RequestVerificationToken]").val();
                    let data = new FormData();
                    data.append("Text", this.searchText);
                    data.append("RecipeTypes", this.searchRecipeTypes);
                    data.append("Ingredients", this.searchIngredients);
                    $.ajax({
                        url: "/Recipe/SideBarSearch",
                        data: data,
                        processData: false,
                        contentType: false,
                        type: "POST",
                        headers: { 'X-CSRF-TOKEN': token.toString() },
                        success: (result) => {
                            $('#partialView').html(result);
                            let fTasteStarsVote = new FiveStarsVote("TasteRateStars");
                            fTasteStarsVote.addEventListener('voteSuccess', this.onHomeVoteSuccess);
                            fTasteStarsVote.startListenToVote();
                            let fEasyStarsVote = new FiveStarsVote("EasyRateStars");
                            fEasyStarsVote.addEventListener('voteSuccess', this.onHomeVoteSuccess);
                            fEasyStarsVote.startListenToVote();
                        }
                    });
                }
            },
            error: function (error) {
                if (error.status == 401) {
                    window.location.href = '/Identity/Account/Login';
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
                url: "/Recipe/DeleteRecipe",
                data: data,
                processData: false,
                contentType: false,
                type: "POST",
                headers: { 'X-CSRF-TOKEN': token.toString() },
                success: (result) => {
                    let token = $("#keyForm input[name=__RequestVerificationToken]").val();
                    let data = new FormData();
                    data.append("Text", this.searchText);
                    data.append("RecipeTypes", this.searchRecipeTypes);
                    data.append("Ingredients", this.searchIngredients);
                    $.ajax({
                        url: "/Recipe/SideBarSearch",
                        data: data,
                        processData: false,
                        contentType: false,
                        type: "POST",
                        headers: { 'X-CSRF-TOKEN': token.toString() },
                        success: (result) => {
                            $('#partialView').html(result);
                            let fTasteStarsVote = new FiveStarsVote("TasteRateStars");
                            fTasteStarsVote.addEventListener('voteSuccess', this.onHomeVoteSuccess);
                            fTasteStarsVote.startListenToVote();
                            let fEasyStarsVote = new FiveStarsVote("EasyRateStars");
                            fEasyStarsVote.addEventListener('voteSuccess', this.onHomeVoteSuccess);
                            fEasyStarsVote.startListenToVote();
                        }
                    });
                },
                error: function (error) {
                    if (error.status == 401) {
                        window.location.href = '/Identity/Account/Login';
                    }
                    else {
                        //TODO show custom error msg
                    }
                }
            });
        }
    }
}
//# sourceMappingURL=recipeIndexView.js.map