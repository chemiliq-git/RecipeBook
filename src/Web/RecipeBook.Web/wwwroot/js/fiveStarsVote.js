class FiveStarsVote extends EventTarget {
    constructor(id) {
        super();
        this.itemId = id;
        this.initStars();
    }
    initStars() {
        let allElements = $('[id^="' + this.itemId + '"]').toArray();
        allElements.forEach(function (Element) {
            let starsElementData = $(Element).data(Const.HTML_ATTRIBUTE_VALUE_KEY);
            let dataArray = starsElementData.split(',');
            let tasteRate = dataArray[1];
            let stars = $(Element).children('li.star');
            for (let i = 0; i < stars.length; i++) {
                if (i < parseInt(tasteRate)) {
                    $(stars[i]).addClass(FiveStarsVote.SELECTED_ELEMENT_CLASS_NAME);
                }
                else {
                    $(stars[i]).removeClass(FiveStarsVote.SELECTED_ELEMENT_CLASS_NAME);
                }
            }
        });
    }
    startListenToVote() {
        //let context = this;
        let allElements = $('[id^="' + this.itemId + '"]').toArray();
        allElements.forEach((Element) => {
            let stars = $(Element).children('li.star');
            for (let i = 0; i < stars.length; i++) {
                stars[i].addEventListener(FiveStarsVote.MOUSE_OVER_EVENT_NAME, (event) => { this.onStartMouseOver(event.target); }, false);
                stars[i].addEventListener(FiveStarsVote.MOUSE_OUT_EVENT_NAME, (event) => { this.onStartMouseOut(event.target); }, false);
                stars[i].addEventListener(FiveStarsVote.CLICK_EVENT_NAME, (event) => { this.onStarClick(event.target); }, false);
            }
        });
    }
    /* 1. Visualizing things on Hover - See next part for action on click */
    onStartMouseOver(currentElement) {
        let starElement = currentElement.parentNode;
        let onStar = parseInt($(starElement).data(Const.HTML_ATTRIBUTE_VALUE_KEY), 10); // The star currently mouse on
        // Now highlight all the stars that's not after the current hovered star
        $(starElement).parent().children('li.star').each(function (index) {
            if (index < onStar) {
                $(this).addClass(FiveStarsVote.HOVER_ELEMENT_CLASS_NAME);
            }
            else {
                $(this).removeClass(FiveStarsVote.HOVER_ELEMENT_CLASS_NAME);
            }
        });
    }
    ;
    onStartMouseOut(currentElement) {
        let starElement = currentElement.parentNode;
        $(starElement).parent().children('li.star').each(function (e) {
            $(this).removeClass(FiveStarsVote.HOVER_ELEMENT_CLASS_NAME);
        });
    }
    ;
    /* 2. Action to perform on click */
    onStarClick(currentElement) {
        let starElement = currentElement.parentNode;
        let onStar = parseInt($(starElement).data(Const.HTML_ATTRIBUTE_VALUE_KEY), 10); // The star currently selected
        //POST
        let parentElementData = $(starElement).parent().data(Const.HTML_ATTRIBUTE_VALUE_KEY);
        let array = parentElementData.split(',');
        let recipeId = array[0];
        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let data = new FormData();
        data.append("RecipeId", recipeId);
        if (this.itemId.indexOf('Taste') >= 0) {
            data.append(FiveStarsVote.TYPE, FiveStarsVote.TASTE);
        }
        else {
            data.append(FiveStarsVote.TYPE, FiveStarsVote.EASY);
        }
        data.append(FiveStarsVote.VALUE, onStar.toString());
        $.ajax({
            url: FiveStarsVote.VOTE_URL,
            data: data,
            processData: false,
            contentType: false,
            type: Const.POST,
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: (result) => {
                let stars = $(starElement).parent().children('li.star');
                let starsValueItem = $(starElement).parent().children('li.list-inline-item');
                if (this.itemId.indexOf('Taste') >= 0) {
                    starsValueItem.html('(Taste rate:' + result + ')');
                }
                else {
                    starsValueItem.html('(Easy rate:' + result + ')');
                }
                for (let i = 0; i < stars.length; i++) {
                    if (i < parseInt(result)) {
                        $(stars[i]).addClass(FiveStarsVote.SELECTED_ELEMENT_CLASS_NAME);
                    }
                    else {
                        $(stars[i]).removeClass(FiveStarsVote.SELECTED_ELEMENT_CLASS_NAME);
                        $(stars[i]).removeClass(FiveStarsVote.HOVER_ELEMENT_CLASS_NAME);
                    }
                }
                this.dispatchEvent(new Event(Const.FIVE_STARS_VOTE_SUCCESS_EVENT_NAME));
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
    ;
}
FiveStarsVote.MOUSE_OVER_EVENT_NAME = 'mouseover';
FiveStarsVote.MOUSE_OUT_EVENT_NAME = 'mouseout';
FiveStarsVote.CLICK_EVENT_NAME = 'click';
//private readonly ERROR_MSG = 'error';
FiveStarsVote.SELECTED_ELEMENT_CLASS_NAME = 'selected';
FiveStarsVote.HOVER_ELEMENT_CLASS_NAME = 'hover';
FiveStarsVote.VOTE_URL = "/api/Vote";
FiveStarsVote.TYPE = 'type';
FiveStarsVote.VALUE = 'value';
FiveStarsVote.TASTE = 'Taste';
FiveStarsVote.EASY = 'Easy';
//# sourceMappingURL=fiveStarsVote.js.map