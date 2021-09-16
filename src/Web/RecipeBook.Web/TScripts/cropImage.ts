class CropImage {    
    private static readonly HIDE = 'hide';
    private static readonly SHOW = 'show';
    private static readonly SRC = 'src';
    private static readonly PREVIEW = '.preview';
    private static readonly SHOW_BS_MODAL = 'shown.bs.modal';
    private static readonly HIDDEN_BS_MODAL = 'hidden.bs.modal';    
    private static readonly CROP = 'crop';

    private static readonly SAMPLE_IMAGE_ID = 'sample_image';
    private static readonly UPLOADED_IMAGE_ID = 'uploaded_image';
    private static readonly IMAGE_PATH_ID = "image_Path";

    private $modal = $('#modal');
    private image = <HTMLImageElement>document.getElementById(CropImage.SAMPLE_IMAGE_ID);
    private cr: any;
    private onImgCroped: (FileReader) => void;

    constructor(onImageCroped: (FileReader) => void) {
        this.onImgCroped = onImageCroped;
        this.$modal.on(CropImage.SHOW_BS_MODAL, () => { this.initCropper() });
        this.$modal.on(CropImage.HIDDEN_BS_MODAL, () => { this.destroyCropper() });
        $('#' + CropImage.CROP).click(() => { this.done() });
    }

    start(data: string) {
        this.image.src = data;
        (<any>this.$modal).modal(CropImage.SHOW)
    }

    stop(data: string) {
        (<any>this.$modal).modal(CropImage.HIDE);
        $("#" + CropImage.UPLOADED_IMAGE_ID).attr(CropImage.SRC, data);
        document.getElementById(CropImage.IMAGE_PATH_ID).setAttribute(Const.HTML_ATTRIBUTE_VALUE_KEY, data);
    }

    private initCropper(this: CropImage) {
        //this.cr = new Cropper(this.image, {
        //    aspectRatio: 1,
        //    viewMode: 0,
        //    preview: CropImage.PREVIEW
        //});

    }

    private destroyCropper(this: CropImage) {
        this.cr.destroy();
        this.cr = null;
    }

    private done(this: CropImage) {       
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
                    }

            });
        }
    };
}