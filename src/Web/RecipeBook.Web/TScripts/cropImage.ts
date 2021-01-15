class cropImage { 
    $modal = $('#modal');
    image = <HTMLImageElement>document.getElementById('sample_image');
    cr: any;
    onImgCroped: any;

    constructor(onImageCroped) {
        let context = this;
        this.onImgCroped = onImageCroped;
        this.$modal.on('shown.bs.modal', function (event) { context.initCropper(context); });
        this.$modal.on('hidden.bs.modal', function (event) { context.destroyCropper(context); });
        $('#crop').click(function (event) { context.done(context); });
    }

    start(data: any) {
        this.image.src = data;
        (<any>this.$modal).modal('show')
    }

    stop(data: any) {        
        (<any>this.$modal).modal('hide');
        $('#uploaded_image').attr('src', data);
        document.getElementById("image_Path").setAttribute("value", data);
    }

    initCropper(context) {
        context.cr = new Cropper(context.image, {
            aspectRatio: 1,
            viewMode: 0,
            preview: '.preview'
        });
       
    }

    destroyCropper(context) {
        context.cr.destroy();
        context.cr = null;
    }

    done(context) {
        let canvas = context.cr.getCroppedCanvas({
            width: 680,
            height: 800
        });

        canvas.toBlob(function (blob) {
            let url = URL.createObjectURL(blob);
            var reader = new FileReader();
            reader.readAsDataURL(blob);
            reader.onloadend =
                function () {
                    context.onImgCroped(reader);
                }

        });
    };
}