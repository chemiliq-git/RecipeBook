$(document).ready(function () {
    let fTasteStarsVote = new FiveStarsVote("TasteRateStars");
    let fEasyStarsVote = new FiveStarsVote("EasyRateStars");
    let crImg = new CropImage(onImageCroped);
    let dragDropImg = new DragDropImage('image_box', onImageDroped, onError);
    dragDropImg.startListen();
    let linkedId = $('#image_box').data('value');
    let token = $("#keyForm input[name=__RequestVerificationToken]").val();
    function onImageDroped(data) {
        crImg.start(data);
    }
    function onError(error) {
    }
    function onImageCroped(reader) {
        let base64data = reader.result;
        let data = new FormData();
        data.append("Image", base64data.toString());
        data.append("Type", "Recipe");
        data.append("LinkedId", linkedId);
        $.ajax({
            url: '/api/image',
            method: 'POST',
            data: data,
            processData: false,
            contentType: false,
            headers: { 'X-CSRF-TOKEN': token.toString() },
            success: function (data) {
                crImg.stop(data);
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
    ;
});
//# sourceMappingURL=recipeCreateView.js.map