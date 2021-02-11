$(document).ready(function () {
    let crImg = new cropImage(onImageCroped);

    let dragDropImg = new dragDropImage('image_box', onImageDroped, onError);
    dragDropImg.startListen();

    let linkedId = $('#image_box').data('value');
    let token = $("#keyForm input[name=__RequestVerificationToken]").val();

    function onImageDroped(data: any) {
        crImg.start(data);
    }

    function onError(error: any) {
    }

    function onImageCroped(reader) {
        var base64data = reader.result;
        let data = new FormData();
        data.append("Image", base64data.toString());
        data.append("Type", "Ingredients");
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
                var er = error;
            }
        });
    };    
});