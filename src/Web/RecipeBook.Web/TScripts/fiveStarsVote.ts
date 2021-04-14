class FiveStarsVote extends EventTarget {
    private static readonly MOUSE_OVER_EVENT_NAME = 'mouseover';
    private static readonly MOUSE_OUT_EVENT_NAME = 'mouseout';
    private static readonly CLICK_EVENT_NAME = 'click';
    //private readonly ERROR_MSG = 'error';
    private static readonly SELECTED_ELEMENT_CLASS_NAME = 'selected'; 
    private static readonly HOVER_ELEMENT_CLASS_NAME = 'hover'; 

    private static readonly VOTE_URL: string = "/api/Vote";

    private static readonly TYPE = 'type';
    private static readonly VALUE = 'value';

    private static readonly TASTE = 'Taste';
    private static readonly EASY = 'Easy';

    private itemId: string    

    constructor(id) {
        super()
        this.itemId = id;
        this.initStars();
    }


    initStars(this: FiveStarsVote) {
        let allElements = $('[id^="' + this.itemId +'"]').toArray();

        allElements.forEach(function (this: FiveStarsVote, Element) {
            let starsElementData: string = $(Element).data(Const.HTML_ATTRIBUTE_VALUE_KEY);
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

    startListenToVote(this: FiveStarsVote) {
        let context = this;
        let allElements = $('[id^="' + this.itemId + '"]').toArray();

        allElements.forEach(function (Element) {

            let stars = $(Element).children('li.star');
            for (let i = 0; i < stars.length; i++) {
                stars[i].addEventListener(FiveStarsVote.MOUSE_OVER_EVENT_NAME, (event) => { context.onStartMouseOver(event.target as HTMLElement); }, false);
                stars[i].addEventListener(FiveStarsVote.MOUSE_OUT_EVENT_NAME, (event) => { context.onStartMouseOut(event.target as HTMLElement); }, false);
                stars[i].addEventListener(FiveStarsVote.CLICK_EVENT_NAME, (event) => { context.onStarClick(event.target as HTMLElement); }, false);
            }
        });
    }

    /* 1. Visualizing things on Hover - See next part for action on click */
    private onStartMouseOver(currentElement: HTMLElement) {
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

    };

    private onStartMouseOut(currentElement: HTMLElement) {
        let starElement = currentElement.parentNode;
        $(starElement).parent().children('li.star').each(function (e) {
            $(this).removeClass(FiveStarsVote.HOVER_ELEMENT_CLASS_NAME);
        });
    };


    /* 2. Action to perform on click */
    private onStarClick(currentElement: HTMLElement) {
        let starElement = currentElement.parentNode;
        let onStar = parseInt($(starElement).data(Const.HTML_ATTRIBUTE_VALUE_KEY), 10); // The star currently selected

        //POST

        let parentElementData: string = $(starElement).parent().data(Const.HTML_ATTRIBUTE_VALUE_KEY);
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
                let starsValueItem = $(starElement).parent().children('li.list-inline-item')
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

    };
}