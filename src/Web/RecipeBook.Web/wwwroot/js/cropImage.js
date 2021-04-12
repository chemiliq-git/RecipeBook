class CropImage {
    constructor(onImageCroped) {
        this.$modal = $('#modal');
        this.image = document.getElementById('sample_image');
        let context = this;
        this.onImgCroped = onImageCroped;
        this.$modal.on('shown.bs.modal', function (event) { context.initCropper(); });
        this.$modal.on('hidden.bs.modal', function (event) { context.destroyCropper(); });
        $('#crop').click(function (event) { context.done(); });
    }
    start(data) {
        this.image.src = data;
        this.$modal.modal('show');
    }
    stop(data) {
        this.$modal.modal('hide');
        $('#uploaded_image').attr('src', data);
        document.getElementById("image_Path").setAttribute("value", data);
    }
    initCropper() {
        this.cr = new Cropper(this.image, {
            aspectRatio: 1,
            viewMode: 0,
            preview: '.preview'
        });
    }
    destroyCropper() {
        this.cr.destroy();
        this.cr = null;
    }
    done() {
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
                    };
            });
        }
    }
    ;
}
//# sourceMappingURL=cropImage.js.map