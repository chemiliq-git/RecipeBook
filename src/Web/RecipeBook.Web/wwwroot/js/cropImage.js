class CropImage {
    constructor(onImageCroped) {
        this.$modal = $('#modal');
        this.image = document.getElementById(CropImage.SAMPLE_IMAGE_ID);
        this.onImgCroped = onImageCroped;
        this.$modal.on(CropImage.SHOW_BS_MODAL, () => { this.initCropper(); });
        this.$modal.on(CropImage.HIDDEN_BS_MODAL, () => { this.destroyCropper(); });
        $('#' + CropImage.CROP).click(() => { this.done(); });
    }
    start(data) {
        this.image.src = data;
        this.$modal.modal(CropImage.SHOW);
    }
    stop(data) {
        this.$modal.modal(CropImage.HIDE);
        $("#" + CropImage.UPLOADED_IMAGE_ID).attr(CropImage.SRC, data);
        document.getElementById(CropImage.IMAGE_PATH_ID).setAttribute(Const.HTML_ATTRIBUTE_VALUE_KEY, data);
    }
    initCropper() {
        //this.cr = new Cropper(this.image, {
        //    aspectRatio: 1,
        //    viewMode: 0,
        //    preview: CropImage.PREVIEW
        //});
    }
    destroyCropper() {
        this.cr.destroy();
        this.cr = null;
    }
    done() {
        let canvas = this.cr.getCroppedCanvas({
            width: 680,
            height: 680
        });
        if (canvas != null) {
            canvas.toBlob((blob) => {
                let url = URL.createObjectURL(blob);
                let reader = new FileReader();
                reader.readAsDataURL(blob);
                reader.onloadend =
                    () => {
                        this.onImgCroped(reader);
                    };
            });
        }
    }
    ;
}
CropImage.HIDE = 'hide';
CropImage.SHOW = 'show';
CropImage.SRC = 'src';
CropImage.PREVIEW = '.preview';
CropImage.SHOW_BS_MODAL = 'shown.bs.modal';
CropImage.HIDDEN_BS_MODAL = 'hidden.bs.modal';
CropImage.CROP = 'crop';
CropImage.SAMPLE_IMAGE_ID = 'sample_image';
CropImage.UPLOADED_IMAGE_ID = 'uploaded_image';
CropImage.IMAGE_PATH_ID = "image_Path";
//# sourceMappingURL=cropImage.js.map