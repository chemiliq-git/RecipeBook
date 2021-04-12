$(document).ready(function () {
    let fTasteStarsVote = new fiveStarsVote("TasteRateStars");   
    let fEasyStarsVote = new fiveStarsVote("EasyRateStars");

    let crImg = new CropImage(onImageCroped);

    let dragDropImg = new dragDropImage('image_box', onImageDroped, onError);
    dragDropImg.startListen();

    

    let linkedId = $('#image_box').data('value');
    let token = $("#keyForm input[name=__RequestVerificationToken]").val();

    function onImageDroped(data: string) {
        crImg.start(data);
    }

    function onError(error: string) {
    }

    function onImageCroped(reader: FileReader) {
        let base64data = reader.result;
        let data = new FormData();
        data.append("Image", base64data.toString());
        data.append("Type", "Recipe");
        data.append("LinkedId", linkedId)
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
    };

});