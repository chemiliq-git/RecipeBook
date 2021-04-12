class CropImage { 
    $modal = $('#modal');
    private image = <HTMLImageElement>document.getElementById('sample_image');
    private cr: Cropper;
    private onImgCroped: (FileReader) => void ;

    constructor(onImageCroped: (FileReader) => void) {
        let context = this;
        this.onImgCroped = onImageCroped;
        this.$modal.on('shown.bs.modal', function (event) { context.initCropper(); });
        this.$modal.on('hidden.bs.modal', function (event) { context.destroyCropper(); });
        $('#crop').click(function (event) { context.done(); });
    }

    start(data: string) {
        this.image.src = data;
        (<any>this.$modal).modal('show')
    }

    stop(data: string) {        
        (<any>this.$modal).modal('hide');
        $('#uploaded_image').attr('src', data);
        document.getElementById("image_Path").setAttribute("value", data);
    }

    private initCropper(this: CropImage) {
        this.cr = new Cropper(this.image, {
            aspectRatio: 1,
            viewMode: 0,
            preview: '.preview'
        });
       
    }

    private destroyCropper(this: CropImage) {
        this.cr.destroy();
        this.cr = null;
    }

    private done(this: CropImage) {
        let context = this;
        let canvas = context.cr.getCroppedCanvas({
            width: 680,
            height: 680
        });

        if (canvas != null) {
            canvas.toBlob(function (blob) {
                let url = URL.createObjectURL(blob);
                let reader = new FileReader();
                reader.readAsDataURL(blob);
                reader.onloadend =
                    () => {
                        context.onImgCroped(reader);
                    }

            });
        }
    };
}