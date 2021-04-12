class fiveStarsVote extends EventTarget {
    private itemName: string    

    constructor(iName) {
        super()
        this.itemName = iName;
        this.initStars();
    }


    initStars() {
        let allElements = $('[id^="' + this.itemName +'"]').toArray();

        allElements.forEach(function (Element) {
            let starsElementData: string = $(Element).data('value');
            let dataArray = starsElementData.split(',');
            let tasteRate = dataArray[1];

            let stars = $(Element).children('li.star');

            for (let i = 0; i < stars.length; i++) {
                if (i < parseInt(tasteRate)) {
                    $(stars[i]).addClass('selected');
                }
                else {
                    $(stars[i]).removeClass('selected');
                }

            }
        });

    }

    startListenToVote(this: fiveStarsVote) {
        let context = this;
        let allElements = $('[id^="' + this.itemName + '"]').toArray();

        allElements.forEach(function (Element) {

            let stars = $(Element).children('li.star');
            for (let i = 0; i < stars.length; i++) {
                stars[i].addEventListener('mouseover', (event) => { context.onStartMouseOver(event.target as HTMLElement); }, false);
                stars[i].addEventListener('mouseout', (event) => { context.onStartMouseOut(event.target as HTMLElement); }, false);
                stars[i].addEventListener('click', (event) => { context.onStarClick(event.target as HTMLElement); }, false);
            }
        });
    }

    /* 1. Visualizing things on Hover - See next part for action on click */
    private onStartMouseOver(currentElement: HTMLElement) {
        let starElement = currentElement.parentNode;
        let onStar = parseInt($(starElement).data('value'), 10); // The star currently mouse on

        // Now highlight all the stars that's not after the current hovered star
        $(starElement).parent().children('li.star').each(function (index) {
            if (index < onStar) {
                $(this).addClass('hover');
            }
            else {
                $(this).removeClass('hover');
            }
        });

    };

    private onStartMouseOut(currentElement: HTMLElement) {
        let starElement = currentElement.parentNode;
        $(starElement).parent().children('li.star').each(function (e) {
            $(this).removeClass('hover');
        });
    };


    /* 2. Action to perform on click */
    private onStarClick(currentElement: HTMLElement) {
        let starElement = currentElement.parentNode;
        let onStar = parseInt($(starElement).data('value'), 10); // The star currently selected

        //POST

        let parentElementData: string = $(starElement).parent().data('value');
        let array = parentElementData.split(',');
        let recipeId = array[0];


        let token = $("#keyForm input[name=__RequestVerificationToken]").val();
        let data = new FormData();
        data.append("RecipeId", recipeId);
        if (this.itemName.indexOf('Taste')>=0) {
            data.append("Type", "Taste");
        }
        else {
            data.append("Type", "Easy");
        }
        data.append("Value", onStar.toString());

        $.ajax({
            url: "/api/vote",
            data: data,
            processData: false,
            contentType: false,
            type: "POST",
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: (result) => {
                let stars = $(starElement).parent().children('li.star');
                let starsValueItem = $(starElement).parent().children('li.list-inline-item')
                if (this.itemName.indexOf('Taste') >= 0) {
                    starsValueItem.html('(Taste rate:' + result + ')');
                }
                else {
                    starsValueItem.html('(Easy rate:' + result + ')');
                }
                

                for (let i = 0; i < stars.length; i++) {
                    if (i < parseInt(result)) {
                        $(stars[i]).addClass('selected');
                    }
                    else {
                        $(stars[i]).removeClass('selected');
                        $(stars[i]).removeClass('hover');
                    }

                }
                this.dispatchEvent(new Event('voteSuccess'));
                
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
}